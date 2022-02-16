USE [VotoXVoto]
GO
/****** Object:  StoredProcedure [dbo].[Users_GetAll_ByMonth]    Script Date: 2/15/2022 7:32:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Users_GetAll_ByMonth]


-- =============================================
-- Author: Moon Park
-- Create date: 11/22/2021
-- Description: Proc to get new users by month for admin dashboard (if you want active users, include WHERE [StatusTypeId] = 1)
-- Code Reviewer: N/A
-- Note: Doesn't take into account months where there may be no new users created
-- =============================================

/*
	Execute [dbo].[Users_GetAll_ByMonth]
*/

as


BEGIN


	Select MONTH(DateCreated) as Month, YEAR(DateCreated) as Year, COUNT(Id) as TotalCount

	FROM dbo.Users
	
	GROUP BY YEAR(DateCreated), MONTH(DateCreated)
	ORDER BY YEAR(DateCreated), MONTH(DateCreated) 
	OFFSET 0 Rows
	FETCH FIRST 6 ROWS ONLY
	

END