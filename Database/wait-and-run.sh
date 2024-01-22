#!/bin/bash

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to be ready"
for i in {1..50};
do
	/opt/mssql-tools/bin/sqlcmd -S library_database -U sa -P SQLserver123! -Q "SELECT 1" > /dev/null 2>&1
	if [ $? -eq 0]
	then 
	    echo "SQL server is ready."
		break
	else
		echo "SQL server not ready yet ..."
		sleep 1
	fi
done

# Seed the DB
/opt/mssql-tools/bin/sqlcmd -S library_database -U sa -P SQLserver123! -i /MyLibrary_DB_Script.sql
