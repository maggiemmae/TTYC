# TTYC

Docker db setup instructions

---Getting the SQL Server Docker Image

$ docker pull mcr.microsoft.com/mssql/server

---Running the Docker Image

$ docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=password" -p 1433:1433 --name SQLServer -d mcr.microsoft.com/mssql/server:latest

---Connecting to the SQL Server From Docker Container

$ docker exec -it SQLServer bash
$ /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "password"
