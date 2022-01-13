//import { products } from '../../data/products'
import axios from 'axios';
import { isConsole } from 'mobile-device-detect';
const state = {
  items: [],
  addToCart: '',
  addedToCart: [],
  removeToCart: '',
  totalCart: 0,
  ascOrder: '',
  filterProducts: [],
  brandFilter: '',
  filterData: '',
  orderBy: '',
  perPage: 12,
  currentPage: 1,
  pagesToShow: 3,
  brands: ['apple', 'huawei', 'sony', 'samsung', 'xiaomi', 'asus'],
  categories: ['Mobile', 'Speaker', 'Furniture', 'Watch'],
  checkoutForm: [],
  checkoutFormAdded: '',
};

const getters = {
  getCheckoutForm: (state) => state.checkoutFormAdded,
  getTotalCart: (state) => state.totalCart,
  getFilterProducts: (state) => state.filterProducts,
  getItems: (state) => state.items,
  getCartItems: (state) => state.addToCart,
  getBrandsItem: (state) => state.brands,
  getCategoryItem: (state) => state.categories,
  getAddToCarts: (state) => state.addedToCart,
  brandsCount(state) {
    const counts = {};
    state.items.forEach((item) => {
      counts[item.brand] = counts[item.brand] + 1 || 1;
      //    console.log(counts)
    });

    return counts;
  },
};

const actions = {
  addCart({ commit }, data) {
    commit('ADD_CART', data);
  },
  addBrandToFilter({ commit }, data) {
    commit('ADD_BRAND_TO_FILTER', data);
  },
  addCategoryItem({ commit }, data) {
    commit('ADD_BRAND_TO_CATEGORY', data);
  },
  removeBrandToFilter({ commit }, data) {
    commit('REMOVE_BRAND_FROM_FILTER', data);
  },
  ascendingOrderCart({ commit }, data) {
    commit('ASCENDING_ORDER_CART', data);
  },
  descendingOrderCart({ commit }, data) {
    commit('DESCENDING_ORDER_CART', data);
  },
  totalCart({ commit }, data) {
    commit('TOTAL_CART', data);
  },
  removeCart({ commit }, data) {
    commit('REMOVE_CART_LIST', data);
  },
  addCheckoutAddress({ commit }, data) {
    commit('ADD_CHECKOUT_FORM', data);
  },
  removeQty({ commit }, data) {
    commit('REMOVE_QTY', data);
  },
  addQty({ commit }, data) {
    commit('ADD_QTY', data);
  },
  fetchProductsFromApi({ commit }) {
    axios.get('https://localhost:5001/api/Shop/getproducts').then((pets) => {
      // console.log('my data is ' , pets.data)
      let data = pets.data;
      commit('GET_PRODUCTS', data);
    });
  },
};

const mutations = {
  GET_PRODUCTS(state, data) {
    state.items = data;
    state.filterProducts = data;
    // console.log("fetched products => " , data);
  },
  ADD_CHECKOUT_FORM(state, data) {
    state.checkoutForm.push(data);
    state.checkoutFormAdded = data;
    console.log(data);
  },
  TOTAL_CART(state, data) {
    state.totalCart = state.addedToCart;
    console.log(state.totalCart.forEach((item) => item.price));
  },
  ADD_CART(state, data) {
    let findId = state.addedToCart.find((product) => product.id == data.id);
    if (findId) {
      // console.log(findId)
      state.totalCart += data.price;
      data['qty'] += 1;
      // console.log(data);
    } else {
      state.totalCart += data.price;
      data['qty'] = 1;
      state.addedToCart.push(data);
    }
  },
  REMOVE_QTY(state, data) {
    let findId = state.addedToCart.find((product) => product.id == data.id);
    if (findId) {
      console.log(data.qty);

      if (data.qty > 1) {
        state.totalCart -= data.price;
        data.qty -= 1;
        console.log(data.qty);
      } else {
        state.totalCart -= data.price;
        let index = state.addedToCart.indexOf(data);
        state.addedToCart.splice(index, 1);
        console.log('not working');
      }
    }
  },
  ADD_QTY(state, data) {
    let findId = state.addedToCart.find((product) => product.id == data.id);
    if (findId) {
      if (data.qty < 10) {
        state.totalCart += data.price;
        data.qty += 1;
      } else {
        console.log('not working');
      }
    }
  },
  ADD_BRAND_TO_FILTER(state, checkedArray) {
    state.filterProducts = state.items.filter((p) =>
      checkedArray.includes(p.brand)
    );
    if (state.filterProducts.length === 0) {
      state.filterProducts = products;
      console.log(state.filterProducts);
    }
  },
  ADD_BRAND_TO_CATEGORY(state, data) {
    state.filterProducts = state.items.filter((p) => p.category == data);
  },
  ASCENDING_ORDER_CART(state) {
    state.items.sort((a, b) => a.price - b.price);
  },
  DESCENDING_ORDER_CART() {
    state.items.sort((a, b) => b.price - a.price);
  },
  REMOVE_CART_LIST(state, data) {
    state.totalCart -= data.qty * data.price;
    //    state.removeAddToCart = state.addedToCart
    let index = state.addedToCart.indexOf(data);
    state.addedToCart.splice(index, 1);

    // console.log(index);
  },
};
export default {
  state,
  getters,
  actions,
  mutations,
};
