SELECT Customers.id as id, Customers.FIO as FIO, Sales.sum as sum
FROM Customers INNER JOIN Sales ON Customers.Id = Sales.customer_id;
--WHERE Customers.FIO = ;