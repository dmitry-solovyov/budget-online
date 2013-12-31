update transactions set transactiontypeid = 4
where id in (
select id 
from transactions where (id in (select parentid from transactionlinks)
	or id in (select childid from transactionlinks))
and currencyId <> (select t2.currencyid from transactions t2 where t2.id in (select childid from transactionlinks where parentid = transactions.id ))
and transactiontypeid in (1,2)
)

update transactions set transactiontypeid = 3
where id in (
select id 
from transactions where (id in (select parentid from transactionlinks)
	or id in (select childid from transactionlinks))
and currencyId = (select t2.currencyid from transactions t2 where t2.id in (select childid from transactionlinks where parentid = transactions.id ))
and transactiontypeid in (1,2)

)

update transactions 
set transactiontypeid = (select transactiontypeid from transactions t2 where t2.id = transactions.linkedid) 
where not linkedid is null
	and id in (select childid from transactionlinks)