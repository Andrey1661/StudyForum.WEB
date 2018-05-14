import './css/site.css';
import 'bootstrap';
import Vue from 'vue';
import App from './App.vue';
import VueRouter from 'vue-router';
import store from "./store/index";
import routes from "./routes";

import vSelect from "vue-select"

Vue.use(VueRouter);
Vue.component('v-select', vSelect);

new Vue({
    el: '#app',
    store,
    router: new VueRouter({ mode: 'history', routes }),
    render: h => h(App)
});
