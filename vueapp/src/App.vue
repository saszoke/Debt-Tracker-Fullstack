<template>
    <v-app  class="mx-auto" style="max-width: 800px">
        <div id="nav">
            <!--<router-link to="/">Home </router-link>
            <router-link to="/login">Login</router-link>-->

        </div>
        <router-view />
      <!--<LoginComponent msg="Bank on the Web" @token="storeToken"/>-->
    </v-app>
</template>

<script>
    //import LoginComponent from './components/LoginComponent.vue'
    //import HomeView from './views/HomeView.vue'
    
export default {
  name: 'App',
  components: {
      //LoginComponent
      //HomeView
    },
    methods: {
        storeToken(token) {
            console.log("Token received: " + token)
        }
    }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
