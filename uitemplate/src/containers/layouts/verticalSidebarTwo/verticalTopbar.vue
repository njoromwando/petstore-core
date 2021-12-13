<template>
  <div class="mb-30 m-4">
    <header
      class="main-header vertical-header bg-white d-flex justify-content-between rounded-0"
    >
      <div class="menu-toggle vertical-toggle" @click="mobileSidebar">
        <div></div>
        <div></div>
        <div></div>
      </div>
      <div style="margin: auto"></div>
      <div class="header-part-right">
        <div class="dropdown">
          <b-dropdown
            id="dropdown-1"
            text="Dropdown Button"
            class="m-md-2 user col align-self-end"
            toggle-class="text-decoration-none"
            no-caret
            variant="link"
          >
            <template slot="button-content">
              <img
                src="@/assets/images/faces/1.jpg"
                id="userDropdown"
                alt
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              />
            </template>

            <div class="dropdown-menu-right" aria-labelledby="userDropdown">
              <div class="dropdown-header">
                <i class="i-Lock-User mr-1"></i> James Njoroge
              </div>
              <!-- <a class="dropdown-item">Account settings</a>
              <a class="dropdown-item">Billing history</a> -->
              <a class="dropdown-item" href="#" @click.prevent="logoutUser"
                >Sign out</a
              >
            </div>
          </b-dropdown>
        </div>
      </div>
      <search-component
        :isSearchOpen.sync="isSearchOpen"
        @closeSearch="toggleSearch"
      ></search-component>
    </header>
  </div>
</template>
<script>
import { mapGetters, mapActions } from "vuex";
import Util from "@/utils";
import searchComponent from "../common/search";
export default {
  components: {
    searchComponent,
  },
  computed: {
    ...mapGetters(["loggedInUser"]),
  },
  data() {
    return {
      userInfo: {},
    };
  },

  methods: {
    ...mapActions(["getLoggedInUser", "signOut"]),
    loggedUser() {
      this.getLoggedInUser();
    },
    logoutUser() {
      this.signOut();

      this.$router.push("/app/sessions/signIn");
    },
  },
  created() {
    this.loggedUser();
  },
};
</script>
>
