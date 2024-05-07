select 
    c.nome, c.cpf
from dbo.Cliente as c with(nolock)
inner join dbo.Financiamento as f with(nolock) on f.cpf = c.cpf
inner join (
    select 
        pf.idfinanciamento,
        count(*) as qdt_parcelas,
        sum(case when p.datapagamento is not null then 1 else 0 end) as parcelas_pagas
    from dbo.ParcelaFinanciamento as pf 
    group by pf.idfinanciamento
    having sum(case when p.datapagamento is not null then 1 else 0 end) / count(*) >= 0.6
) as p with(nolock) on f.idfinanciamento = p.idfinanciamento
where c.estado= 'SP'