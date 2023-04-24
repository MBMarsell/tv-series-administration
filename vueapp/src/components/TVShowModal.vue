<template>
  <div :class="['modal', { 'top-modal': isTopModal }]">
    <div class="modal-content">
      <h2>{{ tvShow.name }}</h2>
      <div class="modal-body">
        <img :src="tvShow.image?.medium" :alt="tvShow?.name" />
        <p v-html="tvShow?.summary"></p>
        <!-- <p>{{ tvShow?.summary }}</p> -->
        <p>
          Rating: {{ tvShow.rating?.average ? tvShow.rating.average : 'N/A' }}
        </p>
        <p>
          Genre:
          {{
            tvShow.genres && tvShow?.genres.length > 0
              ? tvShow.genres[0]
              : 'N/A'
          }}
        </p>
        <p>Network: {{ tvShow.network?.name ? tvShow.network.name : 'N/A' }}</p>
      </div>
      <div class="modal-footer">
        <button @click="$emit('close')" class="modal-close-btn">Close</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    tvShow: {
      type: Object,
      required: true,
    },
  },
  data() {
    return {
      isTopModal: false,
    };
  },
  mounted() {
    const modals = document.querySelectorAll('.modal');
    const topModal = modals[modals.length - 1];
    this.isTopModal = this.$el === topModal;
  },
};
</script>

<style scoped>
.modal {
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 10;
}

.top-modal {
  background-color: rgba(0, 0, 0, 0.4);
}

.modal-content {
  background-color: #fff;
  max-width: 600px;
  width: 80%;
  border-radius: 4px;
  overflow: hidden;
}

.modal-body {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20px;
}

.modal-footer {
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding: 10px;
}

.modal-close-btn {
  padding: 10px;
  border-radius: 4px;
  background-color: #ccc;
  border: none;
  cursor: pointer;
}

.modal-close-btn:hover {
  background-color: #6a6a6a;
}
</style>
