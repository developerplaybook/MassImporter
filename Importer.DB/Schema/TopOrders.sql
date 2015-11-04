CREATE TABLE [dbo].[TopOrders] (
    [NewOrderID]         INT           NOT NULL,
    [NewCustomerId]      NVARCHAR (50) NULL,
    [NewShippingAddress] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([NewOrderID] ASC)
);
