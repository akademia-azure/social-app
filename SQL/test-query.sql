CREATE TABLE TestArticle(
	[Id] INT,
	[Name] NVARCHAR(100),
	[AuthorId] INT
)

DECLARE @counter INT = 0;

WHILE @counter < 100  
BEGIN  
    SET @counter= @counter + 1
   INSERT INTO TestArticle VALUES (@counter, 'Name' + CAST(@counter as NVARCHAR), 300 - @counter)
END  