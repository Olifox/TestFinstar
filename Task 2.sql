1) select C.ClientName,count(CC.Id) ClientCount from Clients C left join ClientContacts CC on CC.ClientId=C.Id group by C.ClientName;

2) select C.ClientName from Clients C right join ClientContacts CC on CC.ClientId=C.Id  group by C.ClientName having count(CC.Id)>2;