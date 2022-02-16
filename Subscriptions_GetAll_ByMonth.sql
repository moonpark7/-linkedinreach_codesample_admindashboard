USE [VotoXVoto]
GO
/****** Object:  StoredProcedure [dbo].[Subscriptions_GetAll_ByMonth]    Script Date: 2/15/2022 7:36:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Subscriptions_GetAll_ByMonth]


-- =============================================
-- Author: Moon Park
-- Create date: 11/22/2021
-- Description: Proc to get new users by month for admin dashboard (if you want active subscriptions, include WHERE s.IsActive = 1)
-- Code Reviewer: N/A
-- Note: Doesn't take into account months where there may be no new subscriptions created
-- =============================================

/*
	Execute [dbo].[Subscriptions_GetAll_ByMonth]

========= TEST CODE BELOW TO CROSS-CHECK ==========

		Select s.CustomerId
				,sp.TransferId
				,sp.DateCreated
				,s.IsActive

	FROM dbo.Subscriptions as s inner join dbo.StripePaymentIntents as sp
								on sp.TransferId like '%' + s.CustomerId + '%'

*/


as


BEGIN


	Select MONTH(sp.DateCreated) as Month, YEAR(sp.DateCreated) as Year, COUNT(sp.AccountId) as TotalCount

	FROM dbo.Subscriptions as s 
	join dbo.StripePaymentIntents as sp on sp.TransferId like '%' + s.CustomerId + '%'
	
	
	GROUP BY YEAR(DateCreated), MONTH(DateCreated)
	ORDER BY YEAR(DateCreated), MONTH(DateCreated) 
	OFFSET 0 Rows
	FETCH FIRST 6 ROWS ONLY


END