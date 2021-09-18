import axios, { AxiosInstance, AxiosRequestConfig } from "axios";

const tocken: string = JSON.parse(sessionStorage.getItem("userInfo") || '""');

const configAxios: AxiosRequestConfig = {
  baseURL: process.env.VUE_APP_BASE_URL_API,
  headers: {
    Authorization: `Bearer ${tocken}`
  },
  validateStatus: function(status) {
    console.log(status);
    return status < 500; // Resolve only if the status code is less than 500
  }
};

const conectApi: AxiosInstance = axios.create(configAxios);

axios.interceptors.request.use(
  function(config) {
    return config;
  },
  function(error) {
    return Promise.reject(error);
  }
);

conectApi.interceptors.request.use(response => {
  return response;
});

//on successful response
conectApi.interceptors.response.use(
  response => response,
  error => {
    return Promise.reject(error);
  }
);

export default conectApi;
