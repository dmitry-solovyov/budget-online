--insert into TransactionLinks (ParentId, ChildId, CreatedBy)
SELECT [Id]
      ,[LinkedId]
      --, case when [Id] < [LinkedId] then 1 else 0 end as Parent
      , 1
  FROM [Budget].[dbo].[Transactions]
  where [LinkedId] is not null
	and [Id] < [LinkedId]
	and not exists (select 1 from TransactionLinks  where parentid = [Transactions].id or childid = [Transactions].id)
  order by id