// import "babel-polyfill";
import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import GullKit from "./plugins/gull.kit";
import store from "./store";
import Breadcumb from "./components/breadcumb";
import firebase from "firebase/app";
import "firebase/auth";
import { firebaseSettings } from "@/data/config";
import i18n from "./lang/lang";
import DateRangePicker from "vue2-daterange-picker";
import "vue2-daterange-picker/dist/vue2-daterange-picker.css";
import "font-awesome/css/font-awesome.min.css";

//defined as global component
Vue.component(
  "VueFontawesome",
  require("vue-fontawesome-icon/VueFontawesome.vue").default
);

Vue.component("breadcumb", Breadcumb);
import InstantSearch from "vue-instantsearch";
Vue.use(InstantSearch);
Vue.use(GullKit);

firebase.initializeApp(firebaseSettings);

Vue.config.productionTip = false;

new Vue({
  store,
  router,
  i18n,
  render: (h) => h(App),
}).$mount("#app");
