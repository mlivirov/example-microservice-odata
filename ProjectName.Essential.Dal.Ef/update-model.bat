dotnet ef dbcontext scaffold^
 "Server=.;Database=ProjectName;Integrated Security=True"^
 Microsoft.EntityFrameworkCore.SqlServer^
 -o ../ProjectName.Essential.Dal.Core/Models^
 --context-dir .\^
 -f -d -c DatabaseContext