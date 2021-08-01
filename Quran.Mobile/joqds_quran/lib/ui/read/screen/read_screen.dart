import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';
import 'package:joqds_quran/bloc/nav/index.dart';

class ReadScreen extends StatelessWidget {
  const ReadScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // appBar: AppBar(leading: BackButton(
      //   onPressed: () {
      //     BlocProvider.of<NavBloc>(context).add(GoHome());
      //   },
      // )),
      persistentFooterButtons: [
        Row(
          children: [
            BackButton(
              onPressed: () {
                BlocProvider.of<NavBloc>(context).add(GoHome());
              },
            ),
          ],
        )
      ],
      body: SafeArea(
        child: BlocBuilder<AyahBloc, AyahState>(
          builder: (context, state) {
            if (state is UnAyahState) {
              return const Center(child: CircularProgressIndicator());
            }
            if (state is ErrorAyahState) {
              return Center(
                  child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Text("خطایی رخ داده (${state.errorMessage})"),
                  ElevatedButton(
                      onPressed: () {
                        BlocProvider.of<AyahBloc>(context)
                            .add(LoadAyahBySurahEvent(state.surahId));
                      },
                      child: const Text("تلاش مجدد"))
                ],
              ));
            }
            if (state is InAyahState) {
              return ListView.separated(
                itemCount: state.ayat.length,
                itemBuilder: (context, index) {
                  var text =
                      "${state.ayat[index].text!} (${state.ayat[index].ayahInSurah})";
                  return Column(
                    children: [
                      if (index == 0 ||
                          state.ayat[index].surahId !=
                              state.ayat[index - 1].surahId)
                        Text(state.ayat[index].surahName!),
                      if (index == 0 ||
                          state.ayat[index].surahId !=
                              state.ayat[index - 1].surahId)
                        Text("بسم الله الرحمن الرحیم",
                            textAlign: TextAlign.center,
                            style: Theme.of(context).textTheme.bodyText1),
                      Text(text,
                          textAlign: TextAlign.center,
                          style: Theme.of(context).textTheme.bodyText1),
                    ],
                  );
                },
                separatorBuilder: (context, index) {
                  return Column(
                    children: <Widget>[
                      if (index > 1 &&
                          state.ayat[index].pageId !=
                              state.ayat[index - 1].pageId)
                        Text("-- صفحه ${state.ayat[index].pageId} --"),
                      // if (index > 1 &&
                      //     state.ayat[index].surahId !=
                      //         state.ayat[index - 1].surahId)
                      //   Text("-- سوره ${state.ayat[index].surahName} --"),
                      if (index > 1 &&
                          state.ayat[index].rubId !=
                              state.ayat[index - 1].rubId)
                        Text(
                            "-- جزء ${state.ayat[index].rubJoz} - حزب ${((state.ayat[index].rubRubInJoz! + 1) / 2).floor()} - ربع ${state.ayat[index].rubRubInJoz} --")
                    ],
                  );
                },
              );
            }
            throw Exception();
          },
        ),
      ),
    );
  }
}
