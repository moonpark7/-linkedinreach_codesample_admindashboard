/*This component is included just as reference; the main components I worked on were 
the AdminSubscriberAnalytics and AdminDashAnalytics*/

import debug from "sabio-debug";
import { Box, Grid, Paper, styled } from "@material-ui/core";
import React, { Fragment } from "react";
import AdminDashbordSlider from "../admin/AdminDashboardSlider";
import AdminSubscriberAnalytics from "./AdminSubscriberAnalytics";
import AdminDashAnalytics from "./AdminDashAnalytics";
import Users from "../users/Users";
import ViewAsTable from "../tables/ViewAsTable";

const _logger = debug.extend("Admin_Dash");

const Item = styled(Paper)(({ theme }) => ({
  ...theme.typography.body2,
  padding: theme.spacing(4),
  textAlign: "center",
  color: theme.palette.text.secondary,
  alignContent: "center",
  position: "100px",
}));

const AdminDashboard = () => {
  _logger("rendering");

  let tableColumns = [
    { id: "givenName", name: "First Name" },
    { id: "surnames", name: "Last Name" },
    { id: "party.name", name: "Party" },
    { id: "electionType.name", name: "Election Type" },
    { id: "isElected", name: "Election Status" },
  ];

  let subscriptionColumns = [
    { id: "id", name: "Id" },
    { id: "email", name: "Email" },
    { id: "subscriptionType", name: "Plan" },
    { id: "subscriptionId", name: "Subscription Id" },
    { id: "isActive", name: "Status" },
  ];

  return (
    <Fragment>
      {/* <CssBaseline /> */}

      <Grid container spacing={10} columns={{ xs: 4, sm: 8, md: 12 }}>
        <Grid item xs={12} sm={12} md={12}>
          <Item>
            <AdminDashbordSlider />
            <ViewAsTable
              endpoint="candidates/paginate"
              subTrue="Elected"
              subFalse="Not Elected"
              columns={tableColumns}
            />
          </Item>
        </Grid>
      </Grid>

      <Grid container spacing={10} columns={{ xs: 4, sm: 8, md: 12 }}>
        <Grid item xs={12} sm={12} md={12}>
          <Item>
            <AdminDashAnalytics />
          </Item>
        </Grid>
      </Grid>

      <Grid container spacing={10} columns={{ xs: 4, sm: 8, md: 12 }}>
        <Grid item xs={12} sm={12} md={12}>
          <Item>
            <Users />
          </Item>
        </Grid>
      </Grid>
      <Grid container spacing={10} columns={{ xs: 4, sm: 8, md: 12 }}>
        <Grid item xs={12} sm={12} md={12}>
          <Item>
            <h3 className="float-left pl-4 text-black">Active Subscriptions</h3>
            <AdminSubscriberAnalytics />
            <ViewAsTable
              endpoint="billing/subscriptions/paginate"
              subTrue="Active"
              subFalse="Not Active"
              columns={subscriptionColumns}
            />
          </Item>
        </Grid>
      </Grid>

      <Box sx={{ flexGrow: 1 }} paddingTop="8px">
        <Grid container spacing={2}></Grid>
      </Box>
    </Fragment>
  );
};

export default AdminDashboard;
