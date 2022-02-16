import React, { Fragment, useEffect, useState } from "react";
import { Grid, Card } from "@material-ui/core";
import Chart from "react-apexcharts";
import billingService from "./billingService";
import userService from "./userService";
import { onGlobalError } from "../../services/serviceHelpers";
import debug from "sabio-debug";

const _logger = debug.extend("adminAnalytics");

/* NOTE to next contributor: The database is set to display the first 6 rows only (or 6 months). 
    Once it hits over 6 rows/months, perhaps consider re-ordering ("ORDER BY...DESC") on SQL databse.
*/

export default function AdminDashAnalytics() {
  const [userCountData, setUserCountData] = useState([]);
  const [subscriptionCountData, setSubscriptionCountData] = useState([]);

  useEffect(() => {
    userService
      .getNewUsersCount()
      .then(onGetUserCountSuccess)
      .catch(onGlobalError);

    billingService
      .getNewSubsCount()
      .then(onGetSubCountSuccess)
      .catch(onGlobalError);
  }, []);

  let onGetUserCountSuccess = (response) => {
    let userCountItems = [];

    userCountItems = response.data.items;
    setUserCountData(userCountItems);

    _logger(response, "user data response");
  };

  let onGetSubCountSuccess = (response) => {
    let subCountItems = [];

    subCountItems = response.items;
    setSubscriptionCountData(subCountItems);

    _logger(response, "subcription data response");
  };

  const getDateLabels = (entry) => {
    let newArr = [];

    entry.map((singleArr) => {
      let month = singleArr.month;
      let year = singleArr.year;
      const date = new Date(Date.UTC(year, month));

      let newDateFormat = new Intl.DateTimeFormat("en-US", {
        month: "short",
        year: "numeric",
      }).format(date);

      newArr.push(newDateFormat);
    });
    return newArr;
  };

  const getValues = (entry) => {
    let newArr = [];

    entry.map((singleArr) => {
      let value = singleArr.totalCount;
      newArr.push(value);
    });
    return newArr;
  };

  const newUsersOptions = {
    chart: {
      toolbar: {
        show: false,
      },
      sparkline: {
        enabled: false,
      },
      stacked: false,
    },
    dataLabels: {
      enabled: false,
    },
    plotOptions: {
      bar: {
        horizontal: false,
        endingShape: "rounded",
        columnWidth: "50%",
      },
    },
    stroke: {
      show: true,
      width: 0,
      colors: ["transparent"],
    },
    colors: ["#0abcce"],
    fill: {
      opacity: 1,
    },
    legend: {
      show: false,
    },
    labels: getDateLabels(userCountData),
    xaxis: {
      crosshairs: {
        width: 1,
      },
    },
  };
  const newUsersData = [
    {
      name: "Newly Added",
      data: getValues(userCountData),
    },
  ];

  const newSubscriptionsOptions = {
    chart: {
      toolbar: {
        show: false,
      },
    },
    stroke: {
      curve: "smooth",
      show: true,
      width: [4],
    },
    colors: ["#1bc943"],
    fill: {
      opacity: 1,
    },
    labels: getDateLabels(subscriptionCountData),
    grid: {
      strokeDashArray: "5",
      borderColor: "rgba(125, 138, 156, 0.3)",
    },
  };
  const newSubscriptionsData = [
    {
      name: "Newly Added",
      data: getValues(subscriptionCountData),
    },
  ];

  return (
    <Fragment>
      <Grid container spacing={4}>
        <Grid item xs={12} lg={6}>
          <Card className="card-box p-4 text-center mb-4">
            <h6 className="text-uppercase font-weight-bold mb-1 text-black">
              New Users
            </h6>
            <div className="py-3">
              <Chart
                options={newUsersOptions}
                series={newUsersData}
                type="bar"
                height={350}
              />
            </div>
          </Card>
        </Grid>
        <Grid item xs={12} lg={6}>
          <Card className="card-box p-4 text-center mb-4">
            <h6 className="text-uppercase font-weight-bold mb-1 text-black">
              New Subscriptions
            </h6>
            <div className="py-3">
              <Chart
                options={newSubscriptionsOptions}
                series={newSubscriptionsData}
                type="line"
                height={350}
              />
            </div>
          </Card>
        </Grid>
      </Grid>
    </Fragment>
  );
}
