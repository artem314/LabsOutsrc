SELECT  Groups.product_group as groupName, SUM(Sales.amount + Purchase.amount) as totalAmount
FROM Groups 
INNER JOIN Sales ON Groups.id = Sales.group_id
INNER JOIN Purchase ON Groups.id = Purchase.group_id
--WHERE Groups.product_group = ''
GROUP BY Groups.product_group
ORDER BY totalAmount;