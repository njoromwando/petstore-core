import firebase from "firebase/app";
import "firebase/auth";
import axios from "axios";
import HTTP from "./http-common";

export default {
  state: {
    loggedInUser:
      localStorage.getItem("userInfo") != null
        ? JSON.parse(localStorage.getItem("userInfo"))
        : null,
    loading: false,
    error: null,
  },
  getters: {
    loggedInUser: (state) => state.loggedInUser,
    loading: (state) => state.loading,
    error: (state) => state.error,
  },
  mutations: {
    setUser(state, data) {
      state.loggedInUser = data;
      state.loading = false;
      state.error = null;
    },
    setLogout(state) {
      state.loggedInUser = null;
      state.loading = false;
      state.error = null;
      // this.$router.go("/");
    },
    setLoading(state, data) {
      state.loading = data;
      state.error = null;
    },
    setError(state, data) {
      state.error = data;
      state.loggedInUser = null;
      state.loading = false;
    },
    clearError(state) {
      state.error = null;
    },
  },
  actions: {
    login({ commit }, data) {
      commit("clearError");
      commit("setLoading", true);
      // var userdetails = JSON.stringify({
      //   username: data.email,
      //   password: data.password,
      // });

      axios
        .post(`https://localhost:5001/api/PetStoreUsers/Login`, {
          username: data.email,
          password: data.password,
        })
        .then((user) => {
          console.log(user);
          const newUser = { uid: user.data.token };
          localStorage.setItem("userInfo", JSON.stringify(newUser));
          commit("setUser", { uid: user.data.token });
          console.log(newUser);
        })
        .catch(function(error) {
          // Handle Errors here.
          // var errorCode = error.code;
          // var errorMessage = error.message;
          // console.log(error);
          localStorage.removeItem("userInfo");
          commit("setError", error);
          // ...
        });
    },

    signUserUp({ commit }, data) {
      commit("setLoading", true);
      commit("clearError");
      firebase
        .auth()
        .createUserWithEmailAndPassword(data.email, data.password)
        .then((user) => {
          commit("setLoading", false);

          const newUser = {
            uid: user.user.uid,
          };
          console.log(newUser);
          localStorage.setItem("userInfo", JSON.stringify(newUser));
          localStorage.setItem("token", newUser.uid);
          commit("setUser", newUser);
        })
        .catch((error) => {
          commit("setLoading", false);
          commit("setError", error);
          localStorage.removeItem("userInfo");
          console.log(error);
        });
    },
    signOut({ commit }) {
      firebase
        .auth()
        .signOut()
        .then(
          () => {
            localStorage.removeItem("userInfo");
            commit("setLogout");
          },
          (_error) => {}
        );
    },
  },
};
