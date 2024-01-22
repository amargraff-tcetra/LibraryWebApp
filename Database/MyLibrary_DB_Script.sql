CREATE DATABASE MyLibrary;

GO

USE MyLibrary;

--AUTHORS TABLE
CREATE TABLE [dbo].[author](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](100) NOT NULL,
	[last_name] [varchar](100) NOT NULL,
	[date_of_birth] [date] NULL,
 CONSTRAINT [PK_author] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

--BOOKS TABLE
CREATE TABLE [dbo].[book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[author_id] [int] NOT NULL,
	[title] [nvarchar](200) NOT NULL,
	[publisher] [nvarchar](200) NOT NULL,
	[publication_date] [date] NOT NULL,
	[paperback] [bit] NOT NULL,
	[copies] [int] NOT NULL,
 CONSTRAINT [PK_book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[book]  WITH CHECK ADD  CONSTRAINT [FK_book_author] FOREIGN KEY([author_id])
REFERENCES [dbo].[author] ([id])

ALTER TABLE [dbo].[book] CHECK CONSTRAINT [FK_book_author]

--INSERT AUTHORS:
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('J.R.R.','Tolkien','1892-01-03'); DECLARE @Tolkien AS INT; SELECT @Tolkien = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Douglas','Adams','1952-03-11'); DECLARE @Adams AS INT; SELECT @Adams = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Orson','Card','1951-08-24'); DECLARE @Card AS INT; SELECT @Card = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Frank','Herbert','1920-10-08'); DECLARE @Herbert AS INT; SELECT @Herbert = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('George','Martin','1948-09-20'); DECLARE @Martin AS INT; SELECT @Martin = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('George','Orwell','1903-06-25'); DECLARE @Orwell AS INT; SELECT @Orwell = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Ray','Bradbury','1920-09-22'); DECLARE @Bradbury AS INT; SELECT @Bradbury = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Isaac','Asimov','1920-01-02'); DECLARE @Asimov AS INT; SELECT @Asimov = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Aldous','Huxley','1894-06-26'); DECLARE @Huxley AS INT; SELECT @Huxley = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Neil','Gaiman','1960-11-10'); DECLARE @Gaiman AS INT; SELECT @Gaiman = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('William','Goldman','1931-08-12'); DECLARE @Goldman AS INT; SELECT @Goldman = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Robert','Jordan','1948-10-17'); DECLARE @Jordan AS INT; SELECT @Jordan = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('William','Gibson','1948-03-17'); DECLARE @Gibson AS INT; SELECT @Gibson = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Stephen','King','1947-09-21'); DECLARE @King AS INT; SELECT @King = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Arthur','Clarke','1917-12-16'); DECLARE @Clarke AS INT; SELECT @Clarke = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Neal','Stephenson','1959-10-31'); DECLARE @Stephenson AS INT; SELECT @Stephenson = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('H.G.','Wells','1866-09-21'); DECLARE @Wells AS INT; SELECT @Wells = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('Jules','Verne','1828-02-08'); DECLARE @Verne AS INT; SELECT @Verne = SCOPE_IDENTITY();
INSERT INTO author (first_name,last_name,date_of_birth) VALUES ('C.S.','Lewis','1898-11-29'); DECLARE @Lewis AS INT; SELECT @Lewis = SCOPE_IDENTITY();

--INSERT BOOKS
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Tolkien,'The Lord Of The Rings Trilogy','Houghton Mifflin','1954-07-29',1,5)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Adams,'The Hitchhikers Guide To The Galaxy','Megadodo Publications','1979-10-12',1,4)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Card,'Enders Game','	Tor Books','1985-01-01',1,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Herbert,'The Dune Chronicles','Chilton Books','1965-08-01',1,4)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Martin,'A Song Of Ice And Fire Series','Bantam Books','1996-01-01',0,5)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Orwell,'1984','Secker & Warburg','1949-06-08',1,2)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Bradbury,'Fahrenheit 451','Ballatine Books','1953-10-01',1,2)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Asimov,'The Foundation Trilogy','Bantam Books','1942-01-01',1,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Huxley,'Brave New World','HarperCollins','1932-01-01',0,2)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Gaiman,'American Gods','William Morrow','2001-06-19',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Goldman,'The Princess Bride','Harcourt Brace Jovanovich','1973-01-01',1,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Jordan,'The Wheel Of Time Series','Dynamite Entertainment','1990-01-01',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Orwell,'Animal Farm','Secker & Warburg','1945-01-01',1,4)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Gibson,'Neuromancer','Ace','1984-07-01',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Asimov,'I, Robot','Gnome Press','1950-12-02',1,4)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@King,'The Dark Tower Series','Marvel Comics','2007-01-01',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Clarke,' 2001: A Space Odyssey','New American Library','1968-01-01',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@King,'The Stand','Doubleday','1978-10-03',1,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Stephenson,'Snow Crash','Bantam Books','1992-06-01',1,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Bradbury,'The Martian Chronicles','Simon & Schuster','1950-05-04',0,4)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Wells,'The Time Machine','William Heinemann','1895-01-01',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Verne,'20,000 Leagues Under The Sea','Simon & Schuster','1869-03-01',0,4)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Wells,'The War Of The Worlds','Harper & Bros','1898-01-01',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Tolkien,'The Silmarillion','Del Rey','1977-09-07',0,3)
INSERT INTO book (author_id, title, publisher, publication_date, paperback,copies) VALUES (@Lewis,'The Space Trilogy','The Bodley Head ','1938-01-01',1,3)