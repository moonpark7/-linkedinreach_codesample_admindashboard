import React, { Fragment, useState, useEffect } from "react";
import { Grid } from "@material-ui/core";
import billingService from "./billingService";
import { onGlobalError } from "../../services/serviceHelpers";
import SingleSubscriptionType from "./SingleSubscriptionType";

export default function AdminSubscriberAnalytics() {
  const [mappedSubscriptions, setMappedSubscriptions] = useState([]);

  const mapSubscriptions = (subscriptionInfo) => {
    return (
      <SingleSubscriptionType
        key={subscriptionInfo.name}
        aSubType={subscriptionInfo}
      ></SingleSubscriptionType>
    );
  };

  useEffect(() => {
    billingService
      .groupedSubscriptions()
      .then(onGetGroupedSuccess)
      .catch(onGlobalError);
  }, []);

  let onGetGroupedSuccess = (response) => {
    const allSubTypes = response.items;
    setMappedSubscriptions(allSubTypes.map(mapSubscriptions));
  };

  return (
    <Fragment>
      <Grid container spacing={4} justify="space-evenly">
        {mappedSubscriptions}
      </Grid>
    </Fragment>
  );
}
