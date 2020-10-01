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
							   Email VARCHAR(70) UNIQUE NULL,
								 CPF VARCHAR(11)        NULL,
						 Autenticado BIT DEFAULT 0      NOT NULL ,
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

	@Nome          VARCHAR(100),
	@Email         VARCHAR(70),
	@CPF           VARCHAR(11),
	@IdResponsavel INT

) AS
BEGIN

	INSERT INTO Cliente(Nome,Email,CPF,IdResponsavel) 
	VALUES (@Nome,@Email,@CPF,@IdResponsavel)

END

GO

--Criando procedure de alteração de cliente.
CREATE OR ALTER PROCEDURE UpdateCliente(

	@IdCliente     INT,
	@Nome          VARCHAR(100),
	@Email         VARCHAR(70),
	@CPF           VARCHAR(11),
	@IdResponsavel INT

) AS
BEGIN

	UPDATE Cliente
	   SET Nome          = @Nome,
		   Email         = @Email,
		   CPF           = @CPF,
		   IdResponsavel = @IdResponsavel
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
CREATE OR ALTER PROCEDURE SelectClieteById(

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

	@Descricao  VARCHAR(150),
	@Preco      DECIMAL(15,2),
	@Observacao VARCHAR(MAX)

) AS
BEGIN

	INSERT INTO Produto(Descricao,Preco,Observacao)
	VALUES (@Descricao,@Preco,@Observacao)

END

GO

--Criando procedure de alteração de produto.
CREATE OR ALTER PROCEDURE UpdateProduto(

	@IdProduto INT,
	@Descricao  VARCHAR(150),
	@Preco      DECIMAL(15,2),
	@Observacao VARCHAR(MAX)

)AS
BEGIN

	UPDATE Produto
	   SET Descricao  = @Descricao,
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

--Clientes
INSERT INTO Cliente (Nome,Email,CPF,IdResponsavel) VALUES ('Brenno Fernando Figueira Segolim','brennosegolim12@gmail.com','49368085803',NULL)
INSERT INTO Cliente (Nome,Email,CPF,IdResponsavel) VALUES ('Pedro Silva','pedro_silva@gmail.com','90915795019',NULL)
INSERT INTO Cliente (Nome,Email,CPF,IdResponsavel) VALUES ('Lucas de Jesus','jesusluquinhas@gmail.com','06300365000',1)

--Acesso
INSERT INTO Acesso ([Login],Senha,Nivel,IdCliente) VALUES ('BFFSegolim','e99a18c428cb38d5f260853678922e03','A',1)
INSERT INTO Acesso ([Login],Senha,Nivel,IdCliente) VALUES ('PSilva','e99a18c428cb38d5f260853678922e03','U',2)
INSERT INTO Acesso ([Login],Senha,Nivel,IdCliente) VALUES ('LJesus','e99a18c428cb38d5f260853678922e03','U',3)

--Produto 
INSERT INTO Produto (Descricao,Preco,Observacao) VALUES ('Salgado',4.00,'Salgado sabores diversos')
INSERT INTO Produto (Descricao,Preco,Observacao) VALUES ('Copo de Refrigerante',2.00,'Coca-Cola ou Guaraná')
INSERT INTO Produto (Descricao,Preco,Observacao) VALUES ('Bolo no pote',5.00,'Bolo de chocolate ou leite ninho.')

--Venda
INSERT INTO Venda ([Data],ValorTotal,IdCliente) VALUES ('23-06-2020',6.00,1)
INSERT INTO Venda ([Data],ValorTotal,IdCliente) VALUES ('23-06-2020',9.00,2)
INSERT INTO Venda ([Data],ValorTotal,IdCliente) VALUES ('23-06-2020',2.00,3)

--Venda_Produto 
INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',1,1,4.00,1)
INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',1,2,2.00,1)
INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',2,1,4.00,1)
INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',2,3,5.00,1)
INSERT INTO Produto_Venda ([Data],IdVenda,IdProduto,Valor,Quantidade) VALUES ('23-06-2020',3,2,2.00,1)

--Selects
SELECT * FROM Cliente
SELECT * FROM Acesso
SELECT * FROM Produto
SELECT * FROM Venda
SELECT * FROM Produto_Venda