import logging
import json
import random
from telegram import Update # type: ignore
from telegram.ext import ApplicationBuilder, ContextTypes, CommandHandler # type: ignore

resources = {}

logging.basicConfig(
    format='%(asctime)s - %(name)s - %(levelname)s - %(message)s',
    level=logging.INFO
)


async def start(update: Update, context: ContextTypes.DEFAULT_TYPE):

    await context.bot.send_message(chat_id=update.effective_chat.id, 
                                   text=random.choice(resources['Greetings']))

async def end(update: Update, context: ContextTypes.DEFAULT_TYPE):

    await context.bot.send_message(chat_id=update.effective_chat.id, 
                                   text=random.choice(resources['Bye']))


if __name__ == '__main__':
    with open ('resources.json', encoding='utf-8') as f:
        resources = json.load(f)
    #logging.getLogger().log(' ' , resources)
    print(resources)
    token = open('token.txt', 'r', encoding='utf-8').read()
    application = ApplicationBuilder().token(token).build()
    start_handler = CommandHandler('start', start)
    application.add_handler(start_handler)
    end_handler = CommandHandler('end', end)
    application.add_handler(end_handler)
    application.run_polling()





