SELECT Suppliers.FIO as FIO, SUM(sum) as sum 
FROM Purchase INNER JOIN Suppliers ON Purchase.supplier_id = Suppliers.Id
--WHERE Suppliers.FIO = N''
GROUP BY FIO;