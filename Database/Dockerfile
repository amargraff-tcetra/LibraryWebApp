FROM mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

COPY ./wait-and-run.sh /wait-and-run.sh
COPY ./MyLibrary_DB_Script.sql /MyLibrary_DB_Script.sql

CMD /wait-and-run.sh