select 
    c.cliente,
    c.cpf
from dbo.cliente as c with(nolock) 
inner join dbo.Financiamento as f with(nolock) on c.cpf = f.cpf
inner join dbo.ParcelaFinanciamento as pf with(nolck) on f.idfinanciamento = pf.idfinanciamento
where pf.datavencimento < DATEADD(day, -5, GETDATE())
and   pf.datapagamento is null
limit 4