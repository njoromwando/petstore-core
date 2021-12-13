import Vue from "vue";
import store from "./store";
// import {isMobile} from "mobile-device-detect";
import Router from "vue-router";
import NProgress from "nprogress";
import authenticate from "./auth/authenticate";

Vue.use(Router);

// create new router

const routes = [
  {
    path: "/",
    component: () => import("./views/app"), //webpackChunkName app
    beforeEnter: authenticate,
    redirect: "/app/apps/products",

    children: [
      //  apps
      {
        path: "/app/apps",
        component: () => import("./views/app/apps"),
        redirect: "/app/apps/chat",
        children: [
          {
            path: "products",
            name: "products",
            component: () => import("./views/app/apps/products"),
          },
          {
            path: "product-detail",
            name: "product-detail",
            component: () => import("./views/app/apps/productDetails"),
          },
          {
            path: "checkout",
            name: "checkout",
            component: () => import("./views/app/apps/cart"),
          },
          {
            path: "checkout-address",
            name: "checkout-address",
            component: () => import("./views/app/apps/checkoutAddress"),
          },
          // {
          //   path: "printInvoice",
          //   name: "printInvoice",
          //   component: () => import("./views/app/apps/printInvoice")
          // },
          // {
          //   path: "taskManager",
          //   name: "taskManager",
          //   component: () => import("./views/app/apps/taskManager")
          // },
          // {
          //   path: "calendar",
          //   name: "calendar",
          //   component: () => import("./views/app/apps/calendar")
          // },
          // {
          //   path: "table",
          //   name: "table",
          //   component: () => import("./views/app/apps/table")
          // },
          // {
          //   path: "vue-table",
          //   path: "vue-table",
          //   component: () => import("./views/app/apps/vue-tables")
          // },
          // {
          //   path: "inbox",
          //   name: "inbox",
          //   component: () => import("./views/app/apps/inbox")
          // },
          // {
          //   path: "chat",
          //   component: () => import("./views/app/apps/chat")
          // },
          // {
          //   path: "contact-details",
          //   component: () => import("./views/app/apps/contact-details")
          // },
          // {
          //   path: "contact-grid",
          //   component: () => import("./views/app/apps/contact-grid")
          // },
          // {
          //   path: "contact-list",
          //   component: () => import("./views/app/apps/contact-list")
          // },
          {
            path: "payment-checkout",
            component: () => import("./views/app/apps/paymentCheckout"),
          },
        ],
      },
    ],
  },
  // sessions
  {
    path: "/app/sessions",
    component: () => import("./views/app/sessions"),
    redirect: "/app/sessions/signIn",
    children: [
      {
        path: "signIn",
        component: () => import("./views/app/sessions/signIn"),
      },
      {
        path: "signUp",
        component: () => import("./views/app/sessions/signUp"),
      },
      {
        path: "forgot",
        component: () => import("./views/app/sessions/forgot"),
      },
    ],
  },

  {
    path: "/vertical-sidebar",
    component: () => import("./containers/layouts/verticalSidebar"),
  },
  {
    path: "*",
    component: () => import("./views/app/pages/notFound"),
  },
];

const router = new Router({
  mode: "history",
  linkActiveClass: "open",
  routes,
  scrollBehavior(to, from, savedPosition) {
    return { x: 0, y: 0 };
  },
});

router.beforeEach((to, from, next) => {
  // If this isn't an initial page load.
  if (to.path) {
    // Start the route progress bar.

    NProgress.start();
    NProgress.set(0.1);
  }
  next();
});

router.afterEach(() => {
  // Remove initial loading
  const gullPreLoading = document.getElementById("loading_wrap");
  if (gullPreLoading) {
    gullPreLoading.style.display = "none";
  }
  // Complete the animation of the route progress bar.
  setTimeout(() => NProgress.done(), 500);
  // NProgress.done();
  // if (isMobile) {
  if (window.innerWidth <= 1200) {
    // console.log("mobile");
    store.dispatch("changeSidebarProperties");
    if (store.getters.getSideBarToggleProperties.isSecondarySideNavOpen) {
      store.dispatch("changeSecondarySidebarProperties");
    }

    if (store.getters.getCompactSideBarToggleProperties.isSideNavOpen) {
      store.dispatch("changeCompactSidebarProperties");
    }
  } else {
    if (store.getters.getSideBarToggleProperties.isSecondarySideNavOpen) {
      store.dispatch("changeSecondarySidebarProperties");
    }

    // store.state.sidebarToggleProperties.isSecondarySideNavOpen = false;
  }
});

export default router;
