<template>
  <div class="main-content">
    <breadcumb :page="'Cart'" :folder="'Ecommerce'" />
    <b-row>
      <b-col md="12" v-if="getAddToCarts.length == ''">
        <b-button variant="primary" size="lg" @click="redirectProduct"
          >Back to Product Page</b-button
        >
      </b-col>
      <b-col md="12" v-else>
        <b-row>
          <b-col md="8">
            <div v-for="(product, key) in getAddToCarts" :key="key">
              <b-card class="mb-30">
                <div
                  class="cart d-flex align-items-center justify-content-between"
                >
                  <div class="cart-image">
                    <img
                      :src="product.imageUrl"
                      style="width: 220px; height: 120px"
                      alt
                    />
                  </div>
                  <div class="cart-description flex-grow-1 ml-4">
                    <h6 class="card-title text-18 mb-2">{{ product.title }}</h6>
                    <p class="cursor-pointer">
                      category:
                      <span class="text-primary">{{ product.category }}</span>
                    </p>
                    <!-- <p class="text-success">In Stock</p> -->
                    <div class="ul-counter-cart">
                      <b-button-group class="mx-1">
                        <b-button
                          variant="ul-btn-cart"
                          @click="removeQty(product)"
                        >
                          <i class="i-Arrow-Left"></i>
                        </b-button>
                        <p class="mr-2 ml-2 mb-0">{{ product.qty }}</p>
                        <b-button
                          variant="ul-btn-cart"
                          @click="addQty(product)"
                        >
                          <i class="i-Arrow-Right"></i>
                        </b-button>
                      </b-button-group>
                    </div>
                  </div>
                  <div class="cart-action text-center">
                    <h5 class="font-weight-700 text-20 mb-4">
                      $ {{ product.price }}
                    </h5>
                    <b-button
                      @click="removeCartPage(product, key)"
                      variant="outline"
                      class="pr-5 pl-4"
                    >
                      <i
                        class="i-Close-Window mr-2 font-weight-700 align-middle text-17"
                      ></i>
                      REMOVE
                    </b-button>
                  </div>
                </div>
              </b-card>
            </div>
          </b-col>
          <b-col md="4">
            <b-card title="Order Summary">
              <div class="ul-cart__order">
                <div class="d-flex justify-content-between mb-2">
                  <p class="font-weight-500 text-16 text-mute m-0">Total :</p>
                  <p class="text-16 m-0">${{ getTotalCart }}</p>
                </div>
                <div class="d-flex justify-content-between mb-2">
                  <p class="font-weight-500 text-16 text-mute m-0">
                    Total Items:
                  </p>
                  <p class="text-16 m-0">{{ getAddToCarts.length }}</p>
                </div>
                <!-- <div class="d-flex justify-content-between">
                  <p class="font-weight-500 text-16 text-mute">Discount:</p>
                  <p class="text-16">5%</p>
                </div> -->
                <!-- <div class="d-flex justify-content-between">
                  <p class="font-weight-500 text-16 text-mute">Delivery Charges:</p>
                  <p class="text-16">Free</p>
                </div> -->
                <hr class="mt-2 mb-2" />

                <div class="d-flex justify-content-between">
                  <p class="font-weight-800 text-16 text-muted">Total:</p>
                  <p class="text-16">${{ getTotalCart }}</p>
                </div>

                <b-button block variant="primary" @click="sendEmail"
                  >Order</b-button
                >
              </div>
            </b-card>
          </b-col>
        </b-row>
      </b-col>
    </b-row>
  </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex';
import axios from 'axios';

export default {
  computed: {
    ...mapGetters(['getAddToCarts', 'getTotalCart', 'getAddToCarts']),
  },
  data() {
    return {
      isCounter: 1,
      token: '',
      minItem: 1,
      clearItems: {},
      selectedItems: {},
      orderDetails: {
        orderDate: '',
        orderNumber: '',
        items: [
          {
            quantity: 0,
            unitPrice: 0,
            productId: '',
            productCategory: '',
            productTitle: '',
            productDescription: '',
          },
        ],
      },
    };
  },
  methods: {
    ...mapActions([
      'totalCart',
      'removeCart',
      'removeQty',
      'addQty',
      'clearCart',
    ]),
    generateRandomOrderNumber(min, max) {
      min = 100;
      max = 100000;
      return Math.floor(Math.random() * (max - min + 1) + min);
      // Math.ceil(Math.random()*1000000)
    },
    currentTime: function () {
      var currentDate = new Date();
      console.log('my date', currentDate);

      var formatted_date = new Date().toJSON();
      console.log('formatted date', formatted_date);
    },

    removeCartPage(product, key) {
      this.$swal({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!',
      }).then((result) => {
        if (result.value) {
          this.removeCart(product);
          this.$swal('Deleted!', 'Your Item has bee Deleted.', 'success');
        }
      });
    },
    redirectProduct() {
      this.$router.push('/app/apps/products');
    },
    orderAddress() {
      this.$router.push('checkout-address');
    },
    sendEmail() {
      console.log('sending data', this.orderDetails);

      // this.orderDetails.orderId = this.generateRandomOrderNumber()
      this.orderDetails.orderDate = new Date().toJSON();
      this.orderDetails.orderNumber = Math.round(+new Date() / 1000).toString();
      // this.restructureCartItems()
      // alert("am here")
      console.log('this orderDetails', this.orderDetails);
      axios
        .post('https://localhost:5001/api/Shop/postorder', this.orderDetails, {
          headers: {
            Authorization: this.token,
          },
        })
        .then((result) => {
          console.log('see if oredr is posted', result.data);
          this.getAddToCarts.splice(0);
          console.log('cleared cart items', this.getAddToCarts);

          this.$bvToast.toast(
            `Your order has been processed successfully. Thank you fro shopping with us. Redirecting ..`,
            {
              title: 'Order Successful!',
              variant: 'success',
              solid: true,
              autoHideDelay: 2000,
            }
          );
        });
      setTimeout(() => {
        this.$router.push('/app/apps/products');
      }, 5000);

      // this.makeToast(
      //     "success",
      //     "You have authorised disbuserment of 7 records listed. You may go ahead and disburse. "
      // );
    },
    makeToast(variant = null, msg) {
      this.$bvToast.toast(msg, {
        title: ` ${variant || 'default'}`,
        variant: variant,
        solid: true,
      });
    },
    restructureCartItems() {
      const newData = this.getAddToCarts.map((a) => {
        return {
          quantity: a.qty,
          unitPrice: a.price,
          productId: a.id,
          productCategory: a.category,
          productTitle: a.title,
          productDescription: a.description,
        };
      });
      console.log('restructured data', newData);
      this.orderDetails.items = newData;
    },
  },

  created() {
    this.currentTime();
    this.restructureCartItems();
    this.token = this.token =
      'Bearer ' + JSON.parse(localStorage.getItem('userInfo')).uid;
    console.log('selected items', this.orderDetails.items);
    console.log('cart items', this.getAddToCarts);
  },
};
</script>
<style></style>
