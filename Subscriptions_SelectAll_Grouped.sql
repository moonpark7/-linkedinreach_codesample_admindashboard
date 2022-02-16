USE [VotoXVoto]
GO
/****** Object:  StoredProcedure [dbo].[Subscriptions_SelectAll_Grouped]    Script Date: 2/15/2022 7:40:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Subscriptions_SelectAll_Grouped]


-- =============================================
-- Author: Moon Park
-- Create date: 11/3/2021
-- Description: proc to get all ACTIVE subscriptions, grouped by type of subscription
-- =============================================

/*
	Execute [dbo].[Subscriptions_SelectAll_Grouped]
*/

as

BEGIN


	
	SELECT COUNT(sps.StripeProductId) as UserCount, p.[Name]
			

	FROM dbo.Subscriptions as s inner join dbo.Users as u
								on s.UserId = u.Id
								inner join dbo.StripeProductSubscriptions as sps
								on s.id = sps.SubscriptionId
								inner join dbo.StripeProducts as p
								on  sps.StripeProductId = p.Id
		WHERE s.IsActive = 1
		GROUP BY p.[Name]
		

END