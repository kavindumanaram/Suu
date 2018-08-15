using Quartz;
using Suu.FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suu.TwitterFetcher
{
    class WordCountJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            using (SuuEntities SuuContext = new SuuEntities())
            {
                var unCountedMessagesList = SuuContext.Status.Where(s => s.is_count == 0).ToList();
                var exsitingMessageTextList = SuuContext.messageCounts.Select(g => g.message_text);
                foreach (var twitterMessage in unCountedMessagesList)
                {
                    foreach (var MesssageTextword in twitterMessage.text.Split(' '))
                    {
                        var word = MesssageTextword.Trim();
                        if (!string.IsNullOrEmpty(word) && !(word[0].ToString() == "@") && (word.Length > 3 && word.Substring(0, 4).ToString() != "http"))
                        {
                            if (!exsitingMessageTextList.Contains(word))
                            {
                                var messageCount = new FrontEnd.Models.messageCount()
                                {
                                    message_text = word,
                                    count = 1
                                };
                                SuuContext.messageCounts.Add(messageCount);
                            }
                            else
                            {
                                var exsitingMessageCountText = SuuContext.messageCounts.Where(d => d.message_text == word).FirstOrDefault();
                                var exsitingMessageCountTextOccurence = exsitingMessageCountText.count;
                                var exsitingMessageCountTextLatestOccurence = exsitingMessageCountTextOccurence + 1;
                                exsitingMessageCountText.count = exsitingMessageCountTextLatestOccurence;
                            }
                        }

                        twitterMessage.is_count = 1;
                        SuuContext.SaveChanges();
                    }
                }
            }
        }
    }
}
