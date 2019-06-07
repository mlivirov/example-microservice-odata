IF "%MSSQL_JDBC_PATH%"=="" (
 SET MSSQL_JDBC_PATH=C:/Program Files/Java/sqljdbc_7.0/enu/mssql-jdbc-7.0.0.jre10.jar
)

SET COMMAND=%1

IF "%COMMAND%"=="" (
 SET COMMAND=update
)

liquibase^
 --driver=com.microsoft.sqlserver.jdbc.SQLServerDriver^
 --classpath="%MSSQL_JDBC_PATH%"^
 --url="jdbc:sqlserver://localhost;databaseName=ProjectName;integratedSecurity=True;"^
 --changeLogFile="./changelog.xml"^
 --contexts=test-data^
 %COMMAND%