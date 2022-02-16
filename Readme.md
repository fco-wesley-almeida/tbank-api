Na pasta raiz, executar os seguintes comandos:

Para executar o código:

dotnet publish -o publish
docker build -t tbank .
docker run -p 5002:5002 tbank

Acessar a documentação no navegador: 
http://localhost:5002/swagger/index.html