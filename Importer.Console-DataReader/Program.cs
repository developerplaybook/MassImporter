using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using Importer.Core;
using NLog;

namespace Importer.Console.DataReader
{
    public class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string connectionString = @"Data Source=pc-samuelnm;Integrated Security=True";
            List<Import> imports = GetImports();
            SqlBulkCopySettings settings = SqlBulkCopySettings.GetSettings();
            EnumerableDataReader enumerableDataReader = new EnumerableDataReader(imports);

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

                        bulkCopy.WriteToServer(enumerableDataReader);
                        Logger.Info("Bulk copy setup sucess");
                       
                    }

                }
            }
            catch (Exception exception)
            {

                Logger.Error("Bulk copy was unable to complete the operation with the follwing error {0}", exception.Message);
            }

           
        }

        private static List<Import> GetImports()
        {
            try
            {
                XDocument xdoc = XDocument.Load(@"c:\bulkuploadorders.xml");

                return xdoc.Descendants("Order").Select(import => new Import
                {
                    OrderID = Convert.ToInt32(import.Element("OrderID").Value),
                    ShipAddress = import.Element("ShipAddress").Value,
                    CustomerID = import.Element("CustomerID").Value
                }).ToList();
            }
            catch (Exception e)
            {
                Logger.Info("Copied {0} so far ...", e.Message);
                throw new Exception("Unable to convert Imports");
            }

        }

        private static void bulkCopy_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            Logger.Info("Copied {0} so far ...", e.RowsCopied);
        }
    }
}
