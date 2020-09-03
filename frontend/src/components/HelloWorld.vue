<template>
  <div>
    <input v-model="currentmessage" placeholder="edit me">
    <li v-for="message in messages" :key="message.childmessage">
                {{ message.childmessage }}
    </li>

    <button type="button" @click="addMessage()">Send</button>
  </div>
</template>

<script>
import {HubConnectionBuilder} from '@microsoft/signalr'

export default {
  data() {
    return{
      messages: [
      {childmessage: '1'},
      {childmessage: '2'}
    ],
      currentmessage: 'Say Hello'
    }
  },
  name: "HelloWorld",
  props: {
    msg: String,
  },
  methods: {
    addMessage(){
      connection.invoke("SendMessageToAll", this.currentmessage).catch(err => console.error(err));
    },
    getMessages(){
      return this.messages
    }
  },

  created() {
    connection.on("Messagelistener", (message) => {
    this.messages.push({ childmessage: message })
})
  }
};

const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:44349/message")
    .build();

async function start() {
    try {
        await connection.start();
        console.log("connected");
    } catch (err) {
        console.log(err);
        setTimeout(() => start(), 5000);
    }
};

connection.onclose(async () => {
    await start();
});

start();


</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
a {
  color: #42b983;
}

button {
    display: inline-block;
    border: none;
    padding: 1rem 2rem;
    margin: 0;
    text-decoration: none;
    background: #0069ed;
    color: #ffffff;
    font-family: sans-serif;
    font-size: 1rem;
    cursor: pointer;
    text-align: center;
    transition: background 250ms ease-in-out, 
                transform 150ms ease;
    -webkit-appearance: none;
    -moz-appearance: none;
}

</style>
