SELECT Customers.FIO as FIO, MAX(Sales.sum) as sum
FROM Customers INNER JOIN Sales ON Customers.Id = Sales.customer_id
--WHERE Customers.FIO = N'Калмыков Евгений Николаевич'
GROUP BY FIO;