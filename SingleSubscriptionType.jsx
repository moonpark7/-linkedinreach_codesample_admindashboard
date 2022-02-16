import React, { Fragment } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { Grid } from "@material-ui/core";
import PropTypes from "prop-types";
import debug from "sabio-debug";

const _logger = debug.extend("subsAnalytics");

const SingleSubscriptionType = (props) => {
  const aSubType = props.aSubType.name;
  const subCount = props.aSubType.userCount;

  _logger(props, "test subscription details");

  return (
    <Fragment>
      <Grid item sm={4}>
        <div className="text-center">
          <div>
            <FontAwesomeIcon
              icon={["far", "user"]}
              className="font-size-xxl text-success"
            />
          </div>
          <div className="mt-3 line-height-sm">
            <b className="font-size-lg">{subCount}</b>
            <span className="text-black-50 d-block">{aSubType}</span>
          </div>
        </div>
      </Grid>
    </Fragment>
  );
};

SingleSubscriptionType.propTypes = {
  aSubType: PropTypes.shape({
    name: PropTypes.string,
    userCount: PropTypes.number,
  }),
};

export default SingleSubscriptionType;
