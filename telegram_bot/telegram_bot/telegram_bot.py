import logging
import json
import random
from telegram import Update # type: ignore
from telegram.ext import ApplicationBuilder, ContextTypes, CommandHandler # type: ignore

RESOURCES = {}

logger = logging.getLogger(__name__);

async def start(update: Update, context: ContextTypes.DEFAULT_TYPE):
    logger.debug('start callback')
    await context.bot.send_message(chat_id=update.effective_chat.id, 
                                   text=random.choice(RESOURCES['Greetings']))

async def end(update: Update, context: ContextTypes.DEFAULT_TYPE):
    logger.debug('end callback')
    await context.bot.send_message(chat_id=update.effective_chat.id, 
                                   text=random.choice(RESOURCES['Bye']))


if __name__ == '__main__':
    logging.basicConfig(level = logging.INFO)
    with open ('resources.json', encoding='utf-8') as f:
        try:
            RESOURCES = json.load(f)
        except Exception as ex:
            logger.error(f'can\'t load resources.json with error: {ex}')
    try:
        token = open('token.txt', 'r', encoding='utf-8').read()
    except Exception as ex:
        logger.error(f'can\'t open token file cause: ')
    
    pass
    application = ApplicationBuilder().token(token).build()
    start_handler = CommandHandler('start', start)
    application.add_handler(start_handler)
    end_handler = CommandHandler('end', end)
    application.add_handler(end_handler)
    application.run_polling()





