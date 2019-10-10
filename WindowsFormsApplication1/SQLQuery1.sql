--select * from dbo.LOT where ID = 'B511-1910001'
--select * from dbo.SFT_OP_EXCEPT where ID = 'B511-1910001'
--select * from dbo.SFT_OP_REALRUN where ID = 'B511-1910001'
--select * from dbo.SFT_SPEC_MODIREC where  MD001 = 'B511-1910001'
--select * from dbo.SFT_TRANSORDER where KEYID = 'B511-1910001'
--select * from SFT_TRANSORDER_LINE where KEYID = 'B511-1910001'
--select * from dbo.SFT_WS_RUN where ID ='B511-1910001'
--select * from dbo.TRANSORDERTYPEDEF
--select * from dbo.OPERATION
select * from dbo.SFT_COLLECTRANGE
select * from dbo.SFT_COLLECTRANGE_LINE
--select * from dbo.SFT_SPEC_MODIREC

select * from dbo.SFT_COLLECTITEM a where   a.CI001 = '04005'
select * from dbo.SFT_COLLECTITEM_LINE a where a.CIL001 = '04005'
select * from dbo.SFT_COLLECTITEM_SUBLINE a where a.CIS004 = '04005'
select * from dbo.SFT_OP_EXCEPT where ID = 'B511-1901001'


--select * from dbo.SFT_OP_EXCEPT where ID = 'B511-1901001'

--select * from dbo.SFT_LOSS_REASON

--select * from dbo.SFT_LOG







--select schema_name(tab.schema_id) + '.' + tab.name as [table], part.rows
--   from sys.tables tab
--        inner join sys.partitions part
--            on tab.object_id = part.object_id
--where part.index_id IN (1, 0) -- 0 - table without PK, 1 table with PK
--group by schema_name(tab.schema_id) + '.' + tab.name,part.rows
--having sum(part.rows) != 0
--order by [table]