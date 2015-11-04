using System.Collections.Generic;

namespace Importer.Core
{
    public class SqlBulkCopySettings
    {
        public int BatchSize { get { return 10; } }
        public int NotifySize { get { return BatchSize*5; } }
        public int NumberOfMergeAttemptsBeforeGivingUp { get { return 2; } }
        public string DestinationTableName { get { return "TopOrders"; } }
        public List<ColumnMapping> ColumnMappings { get; private set; }


        public static SqlBulkCopySettings GetSettings()
        {
            return new SqlBulkCopySettings
            {
                ColumnMappings = new List<ColumnMapping>
                {
                    new ColumnMapping
                    {
                        SourceColumn = "OrderID",
                        DestinationColumn = "NewOrderID"
                    },
                    new ColumnMapping
                    {
                        SourceColumn = "CustomerID",
                        DestinationColumn = "NewCustomerId"
                    },
                    new ColumnMapping
                    {
                        SourceColumn = "ShipAddress",
                        DestinationColumn = "NewShippingAddress"
                    }

                }
            };
        }
    }
}