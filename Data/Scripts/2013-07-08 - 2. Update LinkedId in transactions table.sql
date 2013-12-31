
update transactions 
set linkedid = null
where not linkedid is null and id in (select parentid from transactionlinks)