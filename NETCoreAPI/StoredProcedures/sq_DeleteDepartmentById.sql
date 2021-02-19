USE [EmployeeDB]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetAllDepartmentIdAndName]    Script Date: 2021-02-17 ?? 9:21:46 ******/
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
CREATE PROCEDURE [dbo].[sp_DeleteDepartmentById]
	@DepartmentId INT

AS
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		DELETE Department
		WHERE DepartmentId = @DepartmentId
END
GO


