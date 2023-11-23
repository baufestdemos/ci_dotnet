ALTER TABLE [${schema1}].[Task]
ADD [Description] varchar(250);
GO

CREATE TABLE [${schema1}].[TaskDescriptionHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
    [TaskId] int NOT NULL,
	[BeforeDescription] [varchar](150) NOT NULL,
    [Active] bit default 1 NOT NULL,
    CONSTRAINT [PK_TaskDescriptionHistory] PRIMARY KEY CLUSTERED 
    (
        [Id] ASC
    )
    WITH (PAD_INDEX = OFF, 
    STATISTICS_NORECOMPUTE = OFF, 
    IGNORE_DUP_KEY = OFF, 
    ALLOW_ROW_LOCKS = ON, 
    ALLOW_PAGE_LOCKS = ON, 
    OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [${schema1}].[TaskDescriptionHistory]
ADD CONSTRAINT FK_Task_DescriptionHistory
FOREIGN KEY (TaskId) REFERENCES [${schema1}].[Task](Id);