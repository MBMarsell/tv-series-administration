<template>
  <h1>TV Series task</h1>
  <!-- <TVShow /> -->
  <HomePage />
</template>

<script>
import TVShow from './components/TVShow.vue';
import HomePage from './components/HomePage.vue';

export default {
  name: 'App',
  components: {
    // TVShow,
    HomePage,
  },
};
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
