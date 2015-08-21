--Question 4a)
select distinct p.FirstName, p.LastName, Count(distinct w.WebSiteId)
from Person p 
join UserLoginCredentialValue v on p.PersonId = v.PersonId
join LoginCredential c on v.LoginCredentialId = c.LoginCredentialId
join WebSite w on c.WebSiteId = w.WebSiteId

--Question 4b)
delete v 
from UserLoginCredentialValue v
join LoginCredential c on v.LoginCredentialId = c.LoginCredentialId
join WebSite w on c.WebSiteId = w.WebSiteId
where c.Type = 'password' 
and v.CreatedAt < DATEADD(month, -6, GETDATE()) 
and w.BaseUrl like '%example.com%'
