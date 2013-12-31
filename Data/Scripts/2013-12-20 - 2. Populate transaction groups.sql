
drop table #t
select IDENTITY(int, 1, 1) as Id, t.Id as ParentId, tCh.Id as ChildId, t.SectionId, t.Date, t.TransactionTypeId
	, t.CategoryId, t.Sum as [FromSum], t.AccountId as [FromAccountId], t.CurrencyId as [FromCurrencyId]
	, tCh.Sum as [ToSum], tCh.AccountId as [ToAccountId], tCh.CurrencyId as [ToCurrencyId]
	, t.Description, t.Tags, t.IsDisabled, t.CreatedWhen, t.CreatedBy, t.UpdatedWhen, t.UpdatedBy
into #t
from transactions t
	left outer join TransactionLinks tl on tl.ParentId = t.id
	left outer join transactions tCh on tCh.Id = tl.ChildId
where t.Id not in (select ChildId from TransactionLinks)
	and t.TransactionGroupId is null
	
select * from #t

BEGIN TRANSACTION
SET IDENTITY_INSERT [dbo].[TransactionGroups] ON

insert into TransactionGroups (Id, [SectionId]
,[Date],[TransactionTypeId],[CategoryId],[FromAccountId],[ToAccountId],[FromSum],[ToSum],[FromCurrencyId],[ToCurrenyId],[Description],[Tags]
,[IsDisabled],[CreatedWhen],[CreatedBy],[UpdatedWhen],[UpdatedBy]) 

select Id, [SectionId],[Date],[TransactionTypeId],[CategoryId],[FromAccountId],[ToAccountId],[FromSum],[ToSum],[FromCurrencyId],[ToCurrencyId]
	,[Description],[Tags],[IsDisabled],[CreatedWhen],[CreatedBy],[UpdatedWhen],[UpdatedBy]
from #t 

-- delete from TransactionGroups
-- dbcc checkident (TransactionGroups, reseed, 1)
-- update transactions set TransactionGroupId = NULL

update transactions
set TransactionGroupId = (select Id from #t where #t.ParentId = transactions.id)
where transactions.TransactionGroupId is null

update transactions
set TransactionGroupId = (select Id from #t where #t.ChildId = transactions.id and #t.ChildId is not null)
where exists (select 1 from #t where #t.ChildId = transactions.id)
and transactions.TransactionGroupId is null

SET IDENTITY_INSERT [dbo].[TransactionGroups] OFF

ROLLBACK
COMMIT