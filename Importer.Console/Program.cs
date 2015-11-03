using System;
using System.Data;
using System.Data.SqlClient;
using NLog;

namespace Importer.Console
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=pc-samuelnm;Integrated Security=True";

            DataSet dataSet = new DataSet();

            dataSet.ReadXml(@"c:\bulkuploadorders.xml");

            var sourceDataTable = dataSet.Tables[0];

            Logger.Info("xml file loaded");

            try
            {
                using (SqlConnection destinationConnection = new SqlConnection(connectionString))
                {
                    //open the conncetion
                    destinationConnection.Open();

                    Logger.Info("Database Opened");

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(destinationConnection.ConnectionString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.SqlRowsCopied += bulkCopy_SqlRowsCopied;
                        bulkCopy.NotifyAfter = 10;
                        bulkCopy.BatchSize = 5;
                        bulkCopy.ColumnMappings.Add("OrderID", "NewOrderID");
                        bulkCopy.DestinationTableName = "TopOrders";
                        bulkCopy.WriteToServer(sourceDataTable);
                    }
                }
            }
            catch (Exception exception)
            {
                
                Logger.Error("Bulk copy was unable to complete the operation with the follwing error {0}",exception.Message);
            }
            
        }

        private static void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Logger.Info("Copied {0} so far ...",e.RowsCopied);
        }
    }
}
