SELECT Suppliers.FIO as FIO,Purchase.date as date, Purchase.sum as sum
FROM Purchase INNER JOIN Suppliers ON Purchase.supplier_id = Suppliers.Id
WHERE FIO = N'Иванов Иван Иванович' AND Purchase.date = '2020-05-15';