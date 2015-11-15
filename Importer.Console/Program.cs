using System;
using System.Data;
using System.Data.SqlClient;
using Importer.Core;
using NLog;

namespace Importer.Console
{
   public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            string connectionString = @"Data Source=[SERVER];Integrated Security=True";
            var sourceDataTable = GetSourceDataTable();
            SqlBulkCopySettings settings = SqlBulkCopySettings.GetSettings();


           try
            {
                using (SqlConnection destinationConnection = new SqlConnection(connectionString))
                {
                    destinationConnection.Open();

                    Logger.Info("Database Opened");

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection.ConnectionString))
                    {
                        bulkCopy.SqlRowsCopied += bulkCopy_SqlRowsCopied;
                        bulkCopy.NotifyAfter = settings.NotifySize;
                        bulkCopy.BatchSize = settings.BatchSize;
                        bulkCopy.DestinationTableName = settings.DestinationTableName;
                        foreach (var columnMapping in settings.ColumnMappings)
                        {
                            bulkCopy.ColumnMappings.Add(columnMapping.SourceColumn, columnMapping.DestinationColumn);
                        }
                        bulkCopy.WriteToServer(sourceDataTable);
                        
                        Logger.Info("Bulk copy setup sucess");
                    }

                }
            }
            catch (Exception exception)
            {
                Logger.Error("Bulk copy was unable to complete the operation with the follwing error {0}", exception.Message);
            }


        }

        private static DataTable GetSourceDataTable()
        {
            Logger.Info("Beginning Dataset creation");
            DataSet dataSet = new DataSet();

            dataSet.ReadXml(@"c:\bulkuploadorders.xml");

            var sourceDataTable = dataSet.Tables[0];
            Logger.Info("Ending Dataset creation");
            return sourceDataTable;
        }


        private static void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Logger.Info("Copied {0} so far ...", e.RowsCopied);
        }
    }
}
