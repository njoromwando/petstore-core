import axios from "axios";
axios.defaults.baseURL = "http://localhost:5001/api";
axios.defaults.headers.common["Authorization"] =
  "Bearer " + JSON.parse(localStorage.getItem("userInfo")).uid;
