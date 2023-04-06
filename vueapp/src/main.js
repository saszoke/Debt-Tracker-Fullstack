import { createApp } from 'vue'
import App from './App.vue'
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import store from './store'
import router from './router/index'
import '@mdi/font/css/materialdesignicons.css'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'



const vuetify = createVuetify({
    components,
    directives,
})


const app = createApp(App);

app
    .component('VueDatePicker', VueDatePicker)
    .use(store)
    .use(router)
    .use(vuetify)
    .mount('#app')
