USE [EmployeeDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllDepartmentIdAndName]    Script Date: 2021-02-17 ?? 10:24:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

--IF OBJECT_ID('[dbo].[sp_GetAllDepartmentIdAndName]', 'P') IS NOT NULL
--  DROP PROCEDURE [dbo].[sp_GetAllDepartmentIdAndName]
--GO


CREATE PROCEDURE [dbo].[sp_GetAllDepartmentIdAndName]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DepartmentId, DepartmentName FROM dbo.Department
END
GO


