<template>
    <v-card>
        <v-tabs v-model="tab">
            <v-tab v-for="item in items"
                    :key="item"
                    :value="item">
                {{ item }}
            </v-tab>
        </v-tabs>

        <v-window v-model="tab">
            <v-window-item v-for="item in items"
                           :key="item"
                           :value="item">
                <v-card flat>
                    <v-card-text v-text="text"></v-card-text>
                </v-card>
            </v-window-item>
        </v-window>
    </v-card>
</template>
<script>
  export default {
    data () {
      return {
        tab: null,
        items: [
          'web', 'shopping', 'videos', 'images', 'news',
        ],
        text: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.',
      }
    },
  }
</script>