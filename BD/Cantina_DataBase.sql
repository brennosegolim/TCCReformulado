/* DATABASE */
--Criando base de dados.
IF NOT EXISTS( SELECT Name 
                 FROM sys.databases 
				WHERE Name = 'TCCantina' )
BEGIN

	CREATE DATABASE TCCantina

END

GO

--Selecionando a base de dados para ser usada.
USE TCCantina

GO

/* TABLES */

--Criando a tabela de clientes.
IF NOT EXISTS( SELECT Name 
                 FROM SysObjects
				WHERE Name = 'Cliente' 
				  AND Type = 'U')

BEGIN

	CREATE TABLE Cliente ( IdCliente INT IDENTITY(1,1)  NOT NULL,
								Nome VARCHAR(100)       NOT NULL,
				      DataNascimento DATE               NOT NULL,
					        Telefone VARCHAR(20)        NOT NULL,
							 Celular VARCHAR(20)        NOT NULL,
							   Email VARCHAR(70) UNIQUE NOT NULL,
								 CPF VARCHAR(11)        NOT NULL,
						 Autenticado BIT DEFAULT 0      NOT NULL,
					   IdResponsavel INT                NULL
				   
		   CONSTRAINT Pk_IdCliente PRIMARY KEY CLUSTERED (IdCliente))

END

GO

--Criando a tabela de acesso(login)
IF NOT EXISTS( SELECT Name 
                 FROM SysObjects
				WHERE Name = 'Acesso'
				  AND Type = 'U')

BEGIN

	CREATE TABLE Acesso ( IdAcesso INT IDENTITY(1,1) NOT NULL,
						   [Login] VARCHAR(30)       NOT NULL,
							 Senha VARCHAR(100)       NOT NULL,
							 Nivel VARCHAR(5)        NOT NULL,
						 IdCliente INT               NOT NULL
					 
		   CONSTRAINT PK_IdAcesso PRIMARY KEY CLUSTERED (IdAcesso),
		   CONSTRAINT FK_IdAcesso_IdCliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCLiente))

END

GO

--Tabela de Limite de Gasto
IF NOT EXISTS( SELECT Name 
                 FROM SysObjects
				WHERE Name = 'ClienteLimite'
				  AND Type = 'U')

BEGIN

	CREATE TABLE ClienteLimite ( IdClienteLimite INT IDENTITY(1,1) NOT NULL,
						               IdCliente INT NOT NULL,
							               Valor DECIMAL(15,2) NOT NULL,
							              [Data] DATETIME NOT NULL									
					 
		   CONSTRAINT PK_IdClienteLimite PRIMARY KEY CLUSTERED (IdClienteLimite),
		   CONSTRAINT FK_IdClienteLimite_IdCliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCLiente))

END

GO

--Criando a tabela de produtos.
IF NOT EXISTS( SELECT Name 
                 FROM SysObjects
				WHERE Name = 'Produto' 
				  AND Type = 'U')

BEGIN

	CREATE TABLE Produto ( IdProduto INT IDENTITY(1,1) NOT NULL,
							  Codigo VARCHAR(6) UNIQUE NOT NULL,
						   Descricao VARCHAR(150)      NOT NULL,
							   Preco DECIMAL(15,2)     NOT NULL,
						  Observacao VARCHAR(MAX)      NULL
					  
		   CONSTRAINT PK_IdProduto PRIMARY KEY CLUSTERED (IdProduto))

END

GO

--Criando a tabela de venda.
IF NOT EXISTS( SELECT Name 
                 FROM SysObjects
				WHERE Name = 'Venda' 
				  AND Type = 'U')

BEGIN

	
	CREATE TABLE Venda ( IdVenda INT IDENTITY(1,1) NOT NULL,
						  [Data] DATETIME          NOT NULL,
					  ValorTotal DECIMAL(15,2)     NOT NULL,
					   IdCliente INT               NOT NULL
				  
		   CONSTRAINT PK_IdVenda PRIMARY KEY CLUSTERED (IdVenda),
		   CONSTRAINT FK_IdVenda_IdCliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente))

END

GO


--Criando a tabela de produto_venda.
IF NOT EXISTS( SELECT Name 
                 FROM SysObjects
				WHERE Name = 'Produto_Venda' 
				  AND Type = 'U')

BEGIN

	CREATE TABLE Produto_Venda ( IdProduto_Venda INT IDENTITY(1,1) NOT NULL,
									   IdProduto INT               NOT NULL,
										 IdVenda INT               NOT NULL,
										   Valor DECIMAL(15,2)     NOT NULL,
										  [Data] DATETIME          NOT NULL,
									  Quantidade INT               NOT NULL
								  
			CONSTRAINT PK_IdProduto_Venda PRIMARY KEY CLUSTERED (IdProduto_Venda),
			CONSTRAINT FK_IdProduto_Venda_IdProduto FOREIGN KEY (IdProduto) REFERENCES Produto(IdProduto),
			CONSTRAINT FK_IdProduto_Venda_IdVenda FOREIGN KEY (IdVenda) REFERENCES Venda(IdVenda))

END

GO

/* PROCEDURES */

/* Cliente */

--Criando procedure de inserção de cliente.
CREATE OR ALTER PROCEDURE InsertCliente(

	@Nome           VARCHAR(100),
	@DataNascimento DATE,
	@Telefone       VARCHAR(20),
	@Celular        VARCHAR(20),
	@Email          VARCHAR(70),
	@CPF            VARCHAR(11),
	@IdResponsavel  INT

) AS
BEGIN

	INSERT INTO Cliente(Nome,DataNascimento,Telefone,Celular,Email,CPF,IdResponsavel) 
	VALUES (@Nome,@DataNascimento,@Telefone,@Celular,@Email,@CPF,@IdResponsavel)

END

GO

--Criando procedure de alteração de cliente.
CREATE OR ALTER PROCEDURE UpdateCliente(

	@IdCliente     INT,
	@Nome          VARCHAR(100),
	@DataNascimento DATE,
	@Telefone       VARCHAR(20),
	@Celular        VARCHAR(20),
	@Email         VARCHAR(70),
	@CPF           VARCHAR(11),
	@IdResponsavel INT

) AS
BEGIN

	UPDATE Cliente
	   SET Nome           = @Nome,
	       DataNascimento = @DataNascimento,
		   Telefone       = @Telefone,
		   Celular        = @Celular,
		   Email          = @Email,
		   CPF            = @CPF,
		   IdResponsavel  = @IdResponsavel
	 WHERE IdCliente = @IdCliente

END

GO

--Criando procedure de exclusão de cliente.
CREATE OR ALTER PROCEDURE DeleteCliente(

	@IdCliente INT

) AS
BEGIN

	DELETE FROM Cliente
	 WHERE IdCliente = @IdCliente

END

GO

--Criando procedure para retornar todos clientes.
CREATE OR ALTER PROCEDURE SelectCliente
AS
BEGIN

	SELECT *
	  FROM Cliente
	
END

GO

--Criando procedure para retornar somente um cliente.
CREATE OR ALTER PROCEDURE SelectClienteById(

	@IdCliente INT

)AS
BEGIN

	SELECT *
	  FROM Cliente
	 WHERE IdCliente = @IdCliente

END

GO

/* Acesso */

--Criando procedure de inserção da tabela de Acesso.
CREATE OR ALTER PROCEDURE InsertAcesso(

	@Login VARCHAR(30),
	@Senha VARCHAR(100),
	@IdCliente INT,
	@Nivel VARCHAR(5)

) AS
BEGIN

	INSERT INTO Acesso([Login],Senha,IdCliente,Nivel) 
	VALUES (@Login,@Senha,@IdCliente,@Nivel)

END

GO

--Criando procedure de alteração da tabela de acesso.
CREATE OR ALTER PROCEDURE UpdateAcesso(

	@IdAcesso INT,
	@Login VARCHAR(30),
	@Senha VARCHAR(100),
	@Nivel VARCHAR(5),
	@IdCliente INT

)AS
BEGIN

	UPDATE Acesso
	   SET [Login]   = @Login,
	       Senha     = @Senha,
		   Nivel     = @Nivel,
		   IdCliente = @IdCliente
	 WHERE IdAcesso = @IdAcesso

END

GO

--Criando procedure de exclusão da tabela de acesso.
CREATE OR ALTER PROCEDURE DeleteAcesso(

	@IdAcesso INT

)AS
BEGIN

	DELETE FROM Acesso
	 WHERE IdAcesso = @IdAcesso

END

GO

--Criando procedure de seleção de todos acessos.
CREATE OR ALTER PROCEDURE SelectAcesso
AS
BEGIN

	SELECT *
	  FROM Acesso

END

GO

--Criando procedure de seleção de um único acesso.
CREATE OR ALTER PROCEDURE SelectAcessoById(

	@IdAcesso INT

)AS
BEGIN

	SELECT *
	  FROM Acesso
	 WHERE IdAcesso = @IdAcesso

END

GO

--Criando procedure de seleção de um único acesso.
CREATE OR ALTER PROCEDURE LoginAcesso(

	@Login VARCHAR(30),
	@Senha VARCHAR(100)

)AS
BEGIN

	SELECT COUNT(*) AS Retorno
	  FROM Acesso 
	 WHERE Login = @Login
	   AND Senha = @Senha

END

GO

/* Produto */

--Criando procedure de inserção de produto.
CREATE OR ALTER PROCEDURE InsertProduto(
	
	@Codigo     VARCHAR(6),
	@Descricao  VARCHAR(150),
	@Preco      DECIMAL(15,2),
	@Observacao VARCHAR(MAX)

) AS
BEGIN

	INSERT INTO Produto(Codigo,Descricao,Preco,Observacao)
	VALUES (@Codigo,@Descricao,@Preco,@Observacao)

END

GO

--Criando procedure de alteração de produto.
CREATE OR ALTER PROCEDURE UpdateProduto(

	@IdProduto INT,
	@Codigo     VARCHAR(6),
	@Descricao  VARCHAR(150),
	@Preco      DECIMAL(15,2),
	@Observacao VARCHAR(MAX)

)AS
BEGIN

	UPDATE Produto
	   SET Codigo     = @Codigo,
	       Descricao  = @Descricao,
	       Preco      = @Preco,
		   Observacao = @Observacao
	 WHERE IdProduto  = @IdProduto

END

GO

--Criando procedure de exclusão de produto.
CREATE OR ALTER PROCEDURE DeleteProduto(

	@IdProduto INT

)AS
BEGIN

	DELETE 
	  FROM Produto
     WHERE IdProduto = @IdProduto 

END

GO

--Criando procedure de seleção de produtos.
CREATE OR ALTER PROCEDURE SelectProduto
AS
BEGIN

	SELECT *
	  FROM Produto

END

GO

--Criando procedure de seleção de um único produto.
CREATE OR ALTER PROCEDURE SelectProdutoById(

	@IdProduto INT

)AS
BEGIN
	
	SELECT *
	  FROM Produto
	 WHERE IdProduto = @IdProduto

END

GO

/* Venda */

--Criando procedure de inserção de venda.
CREATE OR ALTER PROCEDURE InsertVenda(

	@Data DATETIME,
	@ValorTotal DECIMAL(15,2),
	@IdCliente INT

)AS
BEGIN

	INSERT INTO Venda([Data],ValorTotal,IdCliente) 
	VALUES (@Data,@ValorTotal,@IdCliente)
	
END

GO

--Criando procedure de alteração de venda.
CREATE OR ALTER PROCEDURE UpdateVenda(

	@IdVenda INT,
	@Data DATETIME,
	@ValorTotal DECIMAL(15,2),
	@IdCliente INT

)AS
BEGIN

	UPDATE Venda
	   SET [Data]     = @Data,
	       ValorTotal = @ValorTotal,
		   IdCliente  = @IdCliente
	 WHERE IdVenda = @IdVenda

END

GO

--Criando procedure de exclusão de venda.
CREATE OR ALTER PROCEDURE DeleteVenda(

	@IdVenda INT

)AS
BEGIN

	DELETE 
	  FROM Venda
	 WHERE IdVenda = @IdVenda

END

GO

--Criando procedure de seleção de vendas.
CREATE OR ALTER PROCEDURE SelectVenda
AS
BEGIN

	SELECT *
	  FROM Venda

END

GO

--Criando procedure de seleção de uma única venda.
CREATE OR ALTER PROCEDURE SelectVendaById(

	@IdVenda INT

)AS
BEGIN

	SELECT *
	  FROM Venda
	 WHERE IdVenda = @IdVenda

END

GO

/* Produto_Venda */

--Criando procedure de inserção de Produto_Venda.
CREATE OR ALTER PROCEDURE InsertProduto_Venda(

	@IdProduto  INT,
	@IdVenda    INT,
	@Valor      DECIMAL,
	@Data       DATETIME,
	@Quantidade INT

)AS
BEGIN

	INSERT INTO Produto_Venda(IdProduto,IdVenda,Valor,[Data],Quantidade)
	VALUES(@IdProduto,@IdVenda,@Valor,@Data,@Quantidade)

END

GO

--Criando procedure de inserção de Produto_Venda.
CREATE OR ALTER PROCEDURE UpdateProduto_Venda(

	@IdProduto_Venda INT,
	@IdProduto       INT,
	@IdVenda         INT,
	@Valor           DECIMAL,
	@Data            DATETIME,
	@Quantidade      INT

)AS
BEGIN

   UPDATE Produto_Venda
	  SET IdProduto  = @IdProduto,
		  IdVenda    = @IdVenda,
		  Valor      = @Valor,
		  [Data]     = @Data,  
		  Quantidade = @Quantidade
	WHERE IdProduto_Venda = @IdProduto_Venda 

END

GO

--Criando procedure de exclusão de Produto_Venda.
CREATE OR ALTER PROCEDURE DeleteProduto_Venda(

	@IdProduto_Venda INT

)AS
BEGIN

	DELETE FROM Produto_Venda
	 WHERE IdProduto_Venda = @IdProduto_Venda

END

GO

--Criando procedure de seleção de Todos itens da tabela Produto_Venda
CREATE OR ALTER PROCEDURE SelectProduto_Venda
AS
BEGIN

	SELECT *
	  FROM Produto_Venda

END

GO

--Criando procedure de seleção de um único item da tabela Produto_Venda
CREATE OR ALTER PROCEDURE SelectProduto_VendaById(

	@IdProduto_Venda INT

)AS
BEGIN

	SELECT *
	  FROM Produto_Venda
	 WHERE IdProduto_Venda = @IdProduto_Venda

END

GO

/* ClienteLimite */

--Criando procedure de inserção de ClienteLimite.
CREATE OR ALTER PROCEDURE InsertClienteLimite(

	@IdCliente       INT,
	@Valor DECIMAL(15,2),
	@Data       DATETIME

)AS
BEGIN

	INSERT INTO ClienteLimite(IdCliente,Valor,[Data])
	VALUES (@IdCliente,@Valor,@Data)

END

GO

--Criando Procedure para selecionar todos registros da tabela ClienteLimite
CREATE OR ALTER PROCEDURE SelectClienteLimite
AS
BEGIN
	
	SELECT *
	  FROM ClienteLimite

END

GO

--Criando Procedure para selecionar um único registro da tabela ClienteLimite
CREATE OR ALTER PROCEDURE SelectClienteLimiteById(

	@IdCliente INT

)
AS
BEGIN
	
	SELECT TOP 1 *
	  FROM ClienteLimite
	 WHERE IdCliente = @IdCliente
	 ORDER BY [Data] DESC

END

GO

/*Diversas*/

--Procedure para retornar o limite e o valor já gasto(diariamente).
CREATE OR ALTER PROCEDURE getValorLimiteDiario(

	@IdCliente INT,
	@Data      DATE

)
AS
BEGIN

	DECLARE @LimiteGasto DECIMAL(15,2),
		    @GastoTotalDiario DECIMAL(15,2)

	SET @LimiteGasto = (SELECT TOP 1 Valor
	                      FROM ClienteLimite 
						 WHERE IdCliente = @IdCliente
						 ORDER BY [Data] DESC)

	SET @GastoTotalDiario = (SELECT SUM(ValorTotal)
	                           FROM Venda
							  WHERE CAST([Data] as date) = CAST(GETDATE() as date)
							    AND IdCliente = @IdCliente)
	                  
	SELECT @LimiteGasto as Limite, @GastoTotalDiario as Gasto

END

--Clientes
IF (SELECT COUNT(*) FROM Cliente) <= 0

BEGIN

	INSERT INTO Cliente (Nome,DataNascimento,Telefone,Celular,Email,CPF,Autenticado,IdResponsavel) VALUES ('Brenno Fernando Figueira Segolim','15/06/1999','(14) 3491-1832','(14) 99737-1965','brennosegolim12@gmail.com','49368085803',1,NULL)
	INSERT INTO Cliente (Nome,DataNascimento,Telefone,Celular,Email,CPF,IdResponsavel) VALUES ('Lucas de Jesus','10/01/1984','(14) 3441-5443','(14) 99899-1344','jesusluquinhas@gmail.com','06300365000',NULL)
	INSERT INTO Cliente (Nome,DataNascimento,Telefone,Celular,Email,CPF,IdResponsavel) VALUES ('Pedro Silva','23/10/2010','(14) 3441-5443','(14) 99703-5465','pedro_silva@gmail.com','90915795019',2)

END

--Acesso
IF (SELECT COUNT(*) FROM Acesso) <= 0

BEGIN

	INSERT INTO Acesso ([Login],Senha,Nivel,IdCliente) VALUES ('BFFSegolim','e99a18c428cb38d5f260853678922e03','A',1)
	INSERT INTO Acesso ([Login],Senha,Nivel,IdCliente) VALUES ('LJesus','e99a18c428cb38d5f260853678922e03','U',2)
	INSERT INTO Acesso ([Login],Senha,Nivel,IdCliente) VALUES ('PSilva','e99a18c428cb38d5f260853678922e03','U',3)

END

--Produto 
IF (SELECT COUNT(*) FROM Produto) <= 0

BEGIN

	INSERT INTO Produto (Codigo,Descricao,Preco,Observacao) VALUES ('1','Salgado',4.00,'Salgado sabores diversos')
	INSERT INTO Produto (Codigo,Descricao,Preco,Observacao) VALUES ('2','Copo de Refrigerante',2.00,'Coca-Cola ou Guaraná')
	INSERT INTO Produto (Codigo,Descricao,Preco,Observacao) VALUES ('3','Bolo no pote',5.00,'Bolo de chocolate ou leite ninho.')

END

--Venda
IF (SELECT COUNT(*) FROM Venda) <= 0

BEGIN

	INSERT INTO Venda ([Data],ValorTotal,IdCliente) VALUES ('23-06-2020',6.00,1)
	INSERT INTO Venda ([Data],ValorTotal,IdCliente) VALUES ('23-06-2020',9.00,2)
	INSERT INTO Venda ([Data],ValorTotal,IdCliente) VALUES ('23-06-2020',2.00,3)

END

IF (SELECT COUNT(*) FROM Produto_Venda) <= 0

BEGIN

	--Produto_Venda 
	INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',1,1,4.00,1)
	INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',1,2,2.00,1)
	INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',2,1,4.00,1)
	INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',2,3,5.00,1)
	INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',3,2,2.00,1)

END

--ClienteLimite
IF (SELECT COUNT(*) FROM ClienteLimite) <= 0

BEGIN

	INSERT INTO ClienteLimite (IdCliente,Valor,Data) VALUES (3,10.00,GETDATE())

END

--Selects
SELECT * FROM Cliente
SELECT * FROM Acesso
SELECT * FROM Produto
SELECT * FROM Venda
SELECT * FROM Produto_Venda
SELECT * FROM ClienteLimite