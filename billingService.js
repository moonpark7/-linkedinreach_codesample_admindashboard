import axios from "axios";
import {
  API_HOST_PREFIX,
  onGlobalError,
  onGlobalSuccess,
} from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}/api/billing`;


export let groupedSubscriptions = () =>{
  const config = {
    method: "GET",
    url: `${endpoint}/subscriptions/grouped`,
    withCredentials: true,
    crossdomain: true,
    headers: {
      "Content-Type": "aplication/json"
    },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

export let getNewSubsCount = () => {
  const config = {
    method: "GET",
    url: `${endpoint}/subscriptions/count`,
    withCredentials: true,
    crossdomain: true,
    headers: {
      "Content-Type": "aplication/json"
    }
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

export default { groupedSubscriptions, getNewSubsCount };