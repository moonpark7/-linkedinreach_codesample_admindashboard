import axios from "axios";
import { API_HOST_PREFIX } from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}/api/users`;

export let getNewUsersCount = () => {
  const config = {
    method: "GET",
    url: `${endpoint}/count`,
    withCredentials: true,
    crossdomain: true,
    headers: {"Content-Type": "application/json"}
  };
  return axios(config);
};

export default { getNewUsersCount};
